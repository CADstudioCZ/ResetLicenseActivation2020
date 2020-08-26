using System.Runtime.Serialization;


//public class ProductInfos
//{
//    public ProductInfo[] Property1 { get; set; }
//}

[DataContract]
public class ProductInfo
{
    [DataMember]
    public bool authorize_succ { get; set; }


    [DataMember]
    public string background { get; set; }


    [DataMember]
    public bool cls_check_succ { get; set; }


    [DataMember]
    public string def_prod_code { get; set; }


    [DataMember]
    public string def_prod_key { get; set; }


    [DataMember]
    public string def_prod_ver { get; set; }


    [DataMember]
    public string feature_id { get; set; }


    //public Highlight highlight { get; set; }


    [DataMember]
    public string icon { get; set; }


    [DataMember]
    public string last_known_expire_date { get; set; }


    [DataMember]
    public int lic_method { get; set; }


    [DataMember]
    public int lic_server_type { get; set; }


    [DataMember]
    public string[] lic_servers { get; set; }


    [DataMember]
    public string sel_prod_code { get; set; }


    [DataMember]
    public string sel_prod_key { get; set; }


    [DataMember]
    public string sel_prod_ver { get; set; }


    [DataMember]
    public string serial_number_nw { get; set; }


    [DataMember]
    public string serial_number_sa { get; set; }


    [DataMember]
    public int[] supported_lic_methods { get; set; }


    [DataMember]
    public bool trial_enabled { get; set; }


    [DataMember]
    public bool user_lic_enabled { get; set; }
}
/*
public class Highlight
{
    public Alpha alpha { get; set; }


    public Blue blue { get; set; }


    public Green green { get; set; }


    public Red red { get; set; }
}


public class Red
{
    public int value { get; set; }
}


public class Green
{
    public int value { get; set; }
}


public class Blue
{
    public int value { get; set; }
}


public class Alpha
{
    public int value { get; set; }
}
*/