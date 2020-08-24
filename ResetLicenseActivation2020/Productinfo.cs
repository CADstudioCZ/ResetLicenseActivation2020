using System.Runtime.Serialization;


//public class ProductInfos
//{
//    public ProductInfo[] Property1 { get; set; }
//}


public class ProductInfo
{
    public bool authorize_succ { get; set; }


    public string background { get; set; }


    public bool cls_check_succ { get; set; }


    public string def_prod_code { get; set; }


    public string def_prod_key { get; set; }


    public string def_prod_ver { get; set; }


    public string feature_id { get; set; }


    //public Highlight highlight { get; set; }


    public string icon { get; set; }


    public string last_known_expire_date { get; set; }


    public int lic_method { get; set; }


    public int lic_server_type { get; set; }


    public string[] lic_servers { get; set; }


    public string sel_prod_code { get; set; }


    public string sel_prod_key { get; set; }


    public string sel_prod_ver { get; set; }


    public string serial_number_nw { get; set; }


    public string serial_number_sa { get; set; }


    public int[] supported_lic_methods { get; set; }


    public bool trial_enabled { get; set; }


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