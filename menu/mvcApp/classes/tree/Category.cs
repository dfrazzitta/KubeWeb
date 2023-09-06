using System.ComponentModel.DataAnnotations.Schema;

namespace mvcApp.classes.tree
{

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual State State { get; set; }
    }

    public class State
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<City> City { get; set; }
    }

    public class TreeViewNode
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public bool checkbox_disabled { get; set; }
    }
    public class Category
    {
        //Cat Id  
        public int ID { get; set; }

        //Cat Name  
        public string? Name { get; set; }

        //Cat Description  
        public string? Description { get; set; }

        //represnts Parent ID and it's nullable  
        public int? Pid { get; set; }
        [ForeignKey("Pid")]
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category>? Childs { get; set; }
    }
}
