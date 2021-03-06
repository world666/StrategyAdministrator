﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SA.VisualizationSystem.BusinessesReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BusinessData", Namespace="http://schemas.datacontract.org/2004/07/StrategyServices.Businesses")]
    [System.SerializableAttribute()]
    public partial class BusinessData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] AddressesListField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BusinessesNamesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] BusinessesNamesListField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] DescriptionsListField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RegionIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Addresses {
            get {
                return this.AddressesField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressesField, value) != true)) {
                    this.AddressesField = value;
                    this.RaisePropertyChanged("Addresses");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] AddressesList {
            get {
                return this.AddressesListField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressesListField, value) != true)) {
                    this.AddressesListField = value;
                    this.RaisePropertyChanged("AddressesList");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BusinessesNames {
            get {
                return this.BusinessesNamesField;
            }
            set {
                if ((object.ReferenceEquals(this.BusinessesNamesField, value) != true)) {
                    this.BusinessesNamesField = value;
                    this.RaisePropertyChanged("BusinessesNames");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] BusinessesNamesList {
            get {
                return this.BusinessesNamesListField;
            }
            set {
                if ((object.ReferenceEquals(this.BusinessesNamesListField, value) != true)) {
                    this.BusinessesNamesListField = value;
                    this.RaisePropertyChanged("BusinessesNamesList");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descriptions {
            get {
                return this.DescriptionsField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionsField, value) != true)) {
                    this.DescriptionsField = value;
                    this.RaisePropertyChanged("Descriptions");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] DescriptionsList {
            get {
                return this.DescriptionsListField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionsListField, value) != true)) {
                    this.DescriptionsListField = value;
                    this.RaisePropertyChanged("DescriptionsList");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RegionId {
            get {
                return this.RegionIdField;
            }
            set {
                if ((this.RegionIdField.Equals(value) != true)) {
                    this.RegionIdField = value;
                    this.RaisePropertyChanged("RegionId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BusinessesReference.IBusinessService")]
    public interface IBusinessService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/GetBusinessesByLanguage", ReplyAction="http://tempuri.org/IBusinessService/GetBusinessesByLanguageResponse")]
        SA.VisualizationSystem.BusinessesReference.BusinessData[] GetBusinessesByLanguage(int languageId, int regionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/GetBusinessesByLanguage", ReplyAction="http://tempuri.org/IBusinessService/GetBusinessesByLanguageResponse")]
        System.Threading.Tasks.Task<SA.VisualizationSystem.BusinessesReference.BusinessData[]> GetBusinessesByLanguageAsync(int languageId, int regionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/GetBusinesses", ReplyAction="http://tempuri.org/IBusinessService/GetBusinessesResponse")]
        SA.VisualizationSystem.BusinessesReference.BusinessData[] GetBusinesses(int regionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/GetBusinesses", ReplyAction="http://tempuri.org/IBusinessService/GetBusinessesResponse")]
        System.Threading.Tasks.Task<SA.VisualizationSystem.BusinessesReference.BusinessData[]> GetBusinessesAsync(int regionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/AddBusinesses", ReplyAction="http://tempuri.org/IBusinessService/AddBusinessesResponse")]
        void AddBusinesses(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/AddBusinesses", ReplyAction="http://tempuri.org/IBusinessService/AddBusinessesResponse")]
        System.Threading.Tasks.Task AddBusinessesAsync(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/EditBusinesses", ReplyAction="http://tempuri.org/IBusinessService/EditBusinessesResponse")]
        void EditBusinesses(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/EditBusinesses", ReplyAction="http://tempuri.org/IBusinessService/EditBusinessesResponse")]
        System.Threading.Tasks.Task EditBusinessesAsync(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/DeleteBusinesses", ReplyAction="http://tempuri.org/IBusinessService/DeleteBusinessesResponse")]
        void DeleteBusinesses(int[] businessIds);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessService/DeleteBusinesses", ReplyAction="http://tempuri.org/IBusinessService/DeleteBusinessesResponse")]
        System.Threading.Tasks.Task DeleteBusinessesAsync(int[] businessIds);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBusinessServiceChannel : SA.VisualizationSystem.BusinessesReference.IBusinessService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BusinessServiceClient : System.ServiceModel.ClientBase<SA.VisualizationSystem.BusinessesReference.IBusinessService>, SA.VisualizationSystem.BusinessesReference.IBusinessService {
        
        public BusinessServiceClient() {
        }
        
        public BusinessServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BusinessServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BusinessServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BusinessServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SA.VisualizationSystem.BusinessesReference.BusinessData[] GetBusinessesByLanguage(int languageId, int regionId) {
            return base.Channel.GetBusinessesByLanguage(languageId, regionId);
        }
        
        public System.Threading.Tasks.Task<SA.VisualizationSystem.BusinessesReference.BusinessData[]> GetBusinessesByLanguageAsync(int languageId, int regionId) {
            return base.Channel.GetBusinessesByLanguageAsync(languageId, regionId);
        }
        
        public SA.VisualizationSystem.BusinessesReference.BusinessData[] GetBusinesses(int regionId) {
            return base.Channel.GetBusinesses(regionId);
        }
        
        public System.Threading.Tasks.Task<SA.VisualizationSystem.BusinessesReference.BusinessData[]> GetBusinessesAsync(int regionId) {
            return base.Channel.GetBusinessesAsync(regionId);
        }
        
        public void AddBusinesses(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList) {
            base.Channel.AddBusinesses(businessesList);
        }
        
        public System.Threading.Tasks.Task AddBusinessesAsync(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList) {
            return base.Channel.AddBusinessesAsync(businessesList);
        }
        
        public void EditBusinesses(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList) {
            base.Channel.EditBusinesses(businessesList);
        }
        
        public System.Threading.Tasks.Task EditBusinessesAsync(SA.VisualizationSystem.BusinessesReference.BusinessData[] businessesList) {
            return base.Channel.EditBusinessesAsync(businessesList);
        }
        
        public void DeleteBusinesses(int[] businessIds) {
            base.Channel.DeleteBusinesses(businessIds);
        }
        
        public System.Threading.Tasks.Task DeleteBusinessesAsync(int[] businessIds) {
            return base.Channel.DeleteBusinessesAsync(businessIds);
        }
    }
}
