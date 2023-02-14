using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace BookWorldStore.Models.Repository
{
    public class EditBookViewModel
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public string des { get; set; }
        public string author { get; set; }
        public int inventory_num { get; set; }
        public string image { get; set; }
        public DateTime publishing_year { get; set; }
        public float price { get; set; }
        List<Category> categories { get; set; }
        List<Supplier> suppliers { get; set; }
        public AppDBContext db { get; set; }
        public EditBookViewModel(AppDBContext _db,int id)
        {
            db = _db;
            this.book_id= id;
            GetInfor(id);
        }

        public List<Category> GetCategory()
        {
            var result= db.categories.ToList();
            List<Category> categories = new List<Category>();
            foreach(var cate in result)
            {
                categories.Add(cate);
            }
            return categories;
        }

        public List<Supplier> GetSupplier()
        {
            List<Supplier> suppliers= new List<Supplier>();
            var result=db.suppliers.ToList();
            foreach(var supplier in result)
            {
                suppliers.Add(supplier);
            }
            return suppliers;
        }

        public void GetInfor(int id)
        {
            var result = db.books.ToList();
           foreach(var book in result)
            {
                if (book.book_id == id)
                {
                    this.title = book.title;
                    this.des = book.des;
                    this.author = book.author;
                    this.inventory_num = book.inventory_num;
                    this.publishing_year = book.publishing_year;
                    this.price = book.price;
                }
            }
        }
    }
}
