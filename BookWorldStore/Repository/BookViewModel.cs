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
        public List<BookViewModel> Showall()
        {

            List<BookViewModel> books = new List<BookViewModel>();
            //var results= db.books.Include(c=>c.category).Include(s=>s.supplier).ToList();
            //foreach (var result in results) {
            //         books.Add(new BookViewModel(db, result.book_id, result.title, result.des, 
            //        result.author, result.inventory_num, result.image, result.publishing_year, result.price,
            //        result.category.cate_id, result.category.name, result.supplier.sup_id, result.supplier.name));
            //}
            return books;
        }
      


    }
}
