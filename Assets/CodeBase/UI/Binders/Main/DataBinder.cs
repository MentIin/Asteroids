using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UniRx;
using CodeBase.UI.Binders.Main; 

namespace CodeBase.UI
{
    public class DataBinder : MonoBehaviour
    {
        #region static
        // static cache
        private static readonly List<BinderInfo> s_binderInfos;
        
        static DataBinder()
        {
            s_binderInfos = new List<BinderInfo>();

            var binderTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => {
                    try { return assembly.GetTypes(); }
                    catch { return Type.EmptyTypes; }
                })
                .Where(type => typeof(IBinder).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var type in binderTypes)
            {
                var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                if (constructors.Length == 1)
                {
                    s_binderInfos.Add(new BinderInfo
                    {
                        BinderType = type,
                        ConstructorParameters = constructors[0].GetParameters()
                    });
                }
            }
        }
        
        #endregion

        [SerializeField] private UnityEngine.Object _targetView;

        private DiContainer _container;
        private readonly Dictionary<string, List<IDisposable>> _bindings = new();
        private readonly Dictionary<Type, object> _viewModels = new();
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Start()
        {
            BindData();
        }

        private void OnDestroy()
        {
            UnbindAll();
        }

        private void BindData()
        {
            var components = _targetView != null 
                ? new[] { _targetView } 
                : GetComponentsInChildren<Component>(true);

            foreach (var component in components)
            {
                if (component == null) continue;
                BindComponent(component);
            }
        }

        private void BindComponent(UnityEngine.Object component)
        {
            var componentType = component.GetType();
            
            var fields = componentType.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var dataAttribute = field.GetCustomAttribute<DataAttribute>();
                if (dataAttribute != null)
                {
                    BindField(component, field, dataAttribute.Id);
                }
            }

            var properties = componentType.GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var dataAttribute = property.GetCustomAttribute<DataAttribute>();
                if (dataAttribute != null)
                {
                    BindProperty(component, property, dataAttribute.Id);
                }
            }
        }

        private void BindField(UnityEngine.Object component, FieldInfo field, string dataId)
        {
            var fieldValue = field.GetValue(component);
            if (fieldValue == null) return;

            var viewModelType = FindViewModelTypeWithDataId(dataId, fieldValue.GetType());
            if (viewModelType != null)
            {
                var viewModel = ResolveViewModel(viewModelType);
                var dataProperty = FindDataProperty(viewModel, dataId);
                if (dataProperty != null)
                {
                    CreateBinding(fieldValue, dataProperty, dataId);
                }
            }
        }

        private void BindProperty(UnityEngine.Object component, PropertyInfo property, string dataId)
        {
            var propertyValue = property.GetValue(component);
            if (propertyValue == null) return;
            
            var viewModelType = FindViewModelTypeWithDataId(dataId, propertyValue.GetType());
            if (viewModelType != null)
            {
                var viewModel = ResolveViewModel(viewModelType);
                var dataProperty = FindDataProperty(viewModel, dataId);
                if (dataProperty != null)
                {
                    CreateBinding(propertyValue, dataProperty, dataId);
                }
            }
        }

        private void CreateBinding(object component, object dataProperty, string dataId)
        {
            Debug.Log("Creating binding for dataId: " + dataId);
            var componentType = component.GetType();
            var propertyType = dataProperty.GetType();

            // search for a suitable binder
            var suitableBinderInfo = s_binderInfos.FirstOrDefault<BinderInfo>(info =>
            {
                if (info.ConstructorParameters.Length != 2) return false;

                var param1Type = info.ConstructorParameters[0].ParameterType;
                var param2Type = info.ConstructorParameters[1].ParameterType;
                
                return param1Type.IsAssignableFrom(componentType) && param2Type.IsAssignableFrom(propertyType);
            });

            if (suitableBinderInfo != null)
            {
                object[] constructorArgs = { component, dataProperty };
                var binder = (IBinder)_container.Instantiate(suitableBinderInfo.BinderType, constructorArgs);
                
                binder.Bind();

                if (!_bindings.ContainsKey(dataId))
                    _bindings[dataId] = new List<IDisposable>();
                
                _bindings[dataId].Add(Disposable.Create(binder.Unbind));
            }
            else
            {
                Debug.LogError($"[DataBinder] No suitable IBinder found with a constructor for types: ({componentType.Name}, {propertyType.Name})");
            }
        }
        
        private Type FindViewModelTypeWithDataId(string dataId, Type expectedType)
        {
            var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => {
                    try { return assembly.GetTypes(); }
                    catch { return Type.EmptyTypes; }
                });
            
            foreach (var type in allTypes)
            {
                if (!typeof(IViewModel).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract)
                    continue;
                Debug.Log("Checking ViewModel type: " + type.Name);
                    
                // properties
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Debug.Log("  Found properties: " + string.Join(", ", properties.Select(p => p.Name)));
                foreach (var property in properties)
                {
                    Debug.Log("  Checking property: " + property.Name);
                    var dataAttribute = property.GetCustomAttribute<DataAttribute>();
                    if (dataAttribute != null && dataAttribute.Id == dataId)
                    {
                        return type;
                    }
                }
                
                // fields
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Debug.Log("  Found fields: " + string.Join(", ", fields.Select(f => f.Name)));
                foreach (var field in fields)
                {
                    Debug.Log("  Checking field: " + field.Name);
                    var dataAttribute = field.GetCustomAttribute<DataAttribute>();
                    Debug.Log("    DataAttribute: " + (dataAttribute));
                    Debug.Log(expectedType + " " +field.FieldType);
                    if (dataAttribute.Id == dataId)
                    {
                        return type;
                    }
                }
            }
            
            Debug.LogWarning($"ViewModel with data ID '{dataId}' and compatible type for '{expectedType.Name}' not found");
            return null;
        }

        private object FindDataProperty(object viewModel, string dataId)
        {
            var type = viewModel.GetType();
            
            // Ищем в свойствах
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var dataAttribute = property.GetCustomAttribute<DataAttribute>();
                if (dataAttribute != null && dataAttribute.Id == dataId)
                {
                    return property.GetValue(viewModel);
                }
            }
            
            // Ищем в полях
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var dataAttribute = field.GetCustomAttribute<DataAttribute>();
                if (dataAttribute != null && dataAttribute.Id == dataId)
                {
                    //todo remove get value
                    Debug.Log(field.GetValue(viewModel));
                    return field.GetValue(viewModel);
                }
            }
            
            return null;
        }
        
        private object ResolveViewModel(Type viewModelType)
        {
            if (!_viewModels.TryGetValue(viewModelType, out var viewModel))
            {
                viewModel = _container.Resolve(viewModelType);
                _viewModels[viewModelType] = viewModel;
            }
            return viewModel;
        }

        private void UnbindAll()
        {
            foreach (var bindingList in _bindings.Values)
            {
                foreach (var binding in bindingList)
                {
                    binding.Dispose();
                }
            }
            _bindings.Clear();
        }
    }
    // class to hold binder type and its constructor parameters
    class BinderInfo
    {
        public Type BinderType;
        public ParameterInfo[] ConstructorParameters;
    }
}