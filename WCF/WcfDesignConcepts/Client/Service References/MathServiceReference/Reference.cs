﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.MathServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MathServiceReference.IMath")]
    public interface IMath {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Add", ReplyAction="http://tempuri.org/IMath/AddResponse")]
        double Add(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Add", ReplyAction="http://tempuri.org/IMath/AddResponse")]
        System.Threading.Tasks.Task<double> AddAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Subtract", ReplyAction="http://tempuri.org/IMath/SubtractResponse")]
        double Subtract(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Subtract", ReplyAction="http://tempuri.org/IMath/SubtractResponse")]
        System.Threading.Tasks.Task<double> SubtractAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Multiple", ReplyAction="http://tempuri.org/IMath/MultipleResponse")]
        double Multiple(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Multiple", ReplyAction="http://tempuri.org/IMath/MultipleResponse")]
        System.Threading.Tasks.Task<double> MultipleAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Divide", ReplyAction="http://tempuri.org/IMath/DivideResponse")]
        double Divide(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMath/Divide", ReplyAction="http://tempuri.org/IMath/DivideResponse")]
        System.Threading.Tasks.Task<double> DivideAsync(double x, double y);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMathChannel : Client.MathServiceReference.IMath, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MathClient : System.ServiceModel.ClientBase<Client.MathServiceReference.IMath>, Client.MathServiceReference.IMath {
        
        public MathClient() {
        }
        
        public MathClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MathClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MathClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MathClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double Add(double x, double y) {
            return base.Channel.Add(x, y);
        }
        
        public System.Threading.Tasks.Task<double> AddAsync(double x, double y) {
            return base.Channel.AddAsync(x, y);
        }
        
        public double Subtract(double x, double y) {
            return base.Channel.Subtract(x, y);
        }
        
        public System.Threading.Tasks.Task<double> SubtractAsync(double x, double y) {
            return base.Channel.SubtractAsync(x, y);
        }
        
        public double Multiple(double x, double y) {
            return base.Channel.Multiple(x, y);
        }
        
        public System.Threading.Tasks.Task<double> MultipleAsync(double x, double y) {
            return base.Channel.MultipleAsync(x, y);
        }
        
        public double Divide(double x, double y) {
            return base.Channel.Divide(x, y);
        }
        
        public System.Threading.Tasks.Task<double> DivideAsync(double x, double y) {
            return base.Channel.DivideAsync(x, y);
        }
    }
}
