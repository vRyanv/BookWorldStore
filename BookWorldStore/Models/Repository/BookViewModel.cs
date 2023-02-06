using BookWorldStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWorldStore.Models.Repository
{
    public class BookViewModel
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public string des { get; set; }
        public string author { get; set; }
        public int inventory_num { get; set; }
        public string image { get; set; }
        public DateTime publishing_year { get; set; }
        public float price { get; set; }
        public int cate_id { get; set; }
        public string cate_name { get; set; }
        public int sup_id { get; set; }
        public string sup_name { get; set;}
        public AppDBContext db { get; set; }
        public BookViewModel(AppDBContext db)
        {
            this.db = db;
        }
        public BookViewModel(AppDBContext appDBContext, int book_id, string title, string des,
            string author, int inventory_num, string image, DateTime publishing_year, 
            float price, int cate_id,string cate_name, int sup_id, string sup_name)
        {
            db = appDBContext;
            this.book_id= book_id;
            this.title= title;
            this.des= des;
            this.author= author;
            this.inventory_num = inventory_num;
            this.image = image;
            this.publishing_year= publishing_year;
            this.price = price;
            this.cate_id= cate_id;
            this.cate_name= cate_name;
            this.sup_id = sup_id;
            this.sup_name = sup_name;

        }
        
     
        public List<BookViewModel> Showall()
        {
            
            List<BookViewModel> books= new List<BookViewModel>();
            var results= db.books.Include(c=>c.category).Include(s=>s.supplier).ToList();
            foreach (var result in results) {
                     books.Add(new BookViewModel(db, result.book_id, result.title, result.des, 
                    result.author, result.inventory_num, result.image, result.publishing_year, result.price,
                    result.category.cate_id, result.category.name, result.supplier.sup_id, result.supplier.name));
            }
            //var result = (from b in db.books
            //              join c in db.categories
            //              on b.category.cate_id equals c.cate_id
            //              join s in db.suppliers
            //              on b.supplier.sup_id equals s.sup_id
            //              select new
            //              {
            //                  book = new BookViewModel(db, b.book_id, b.title, b.des,
            //              b.author, b.inventory_num,b.image,b.publishing_year,
            //              b.price, c.cate_id, c.name, s.sup_id,s.name)
            //              }
            //             ).ToList();

            return books;
        }
      


    }
}
