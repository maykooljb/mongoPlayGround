

namespace mongoConsole.Models {
public class Users: BaseMongoModel {   

    public string UserName {get; set;}
    
    public NameModel Name { get; set; }
    }
}
