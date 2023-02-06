using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BookWorldStore.Models.Repository
{
    public class StatisticalHotBookViewModel
    {
        public int Id { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public AppDBContext db { get; set; }

        public StatisticalHotBookViewModel(AppDBContext db)
        {
            this.db = db;
        }

        public StatisticalHotBookViewModel(AppDBContext db, int id,string title, int quantity )
        {
            this.Id = id;
            this.title = title;
            this.quantity = quantity;
            this.db = db;
        }

        public  List<StatisticalHotBookViewModel> showHistory()
        {
            List<StatisticalHotBookViewModel> hotBooks=new List<StatisticalHotBookViewModel> ();
            var result = (from od in db.orderDetails join b in db.books on od.book.book_id equals b.book_id
                          group new {od,b } by b.book_id into g
                          select new
                          {
                              Id=g.Key,
                              quantity=g.Sum(x=>x.od.quantity)
                          }).ToList().OrderByDescending(x=>x.quantity).Take(5);
            foreach (var hotbook in result)
            {
                hotBooks.Add(new StatisticalHotBookViewModel(db, hotbook.Id, getTitle(hotbook.Id),hotbook.quantity));
            }
          
            return hotBooks;
        }
        public string getTitle(int book_id)
        {
            var query= db.books.Where(b=> b.book_id == book_id).Select(b=> new
            {
                title=b.title,
            }).FirstOrDefault();
            if (query!=null)
            {
                return query.title;
            }
            else
                return "Not found";
            
        }
    }
}
