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
        public BookViewModel()
        {

        }
        
     
        public List<BookViewModel> Showall(AppDBContext _context)
        {
            List<BookViewModel> list = new List<BookViewModel>();
            foreach (var book in _context.books.Include("Category").Include("Supplier"))
            {
                BookViewModel B = new BookViewModel();
                B.book_id= book.book_id;
                B.title= book.title;
                B.sup_name = book.supplier.name;
                list.Add(B);
            }
           
            return list;
        }

    }
}
