﻿using System.Xml.Serialization;

[System.Serializable()]
public class NDependMetricDefinition {
    
    private string nDependMetricTypeField;
    
    private string propertyNameField;
    
    private string internalPropertyNameField;
    
    private string metricNameField;
    
    private string descriptionField;

    [XmlElement("NDependMetricType")]
    public string NDependMetricType {
        get {
            return this.nDependMetricTypeField;
        }
        set {
            this.nDependMetricTypeField = value;
        }
    }


    [XmlElement("PropertyName")]
    public string PropertyName {
        get {
            return this.propertyNameField;
        }
        set {
            this.propertyNameField = value;
        }
    }


    [XmlElement("InternalPropertyName")]
    public string InternalPropertyName {
        get {
            return this.internalPropertyNameField;
        }
        set {
            this.internalPropertyNameField = value;
        }
    }


    [XmlElement("MetricName")]
    public string MetricName {
        get {
            return this.metricNameField;
        }
        set {
            this.metricNameField = value;
        }
    }
    

    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Description {
        get {
            return this.descriptionField;
        }
        set {
            this.descriptionField = value;
        }
    }
}
