using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hotel.Data;
using System.Net.Http.Headers;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly APIContext _context;
        public BookingController(APIContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Booking booking) 
        {
            if(booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDB = _context.Bookings.FirstOrDefault(x => x.Id == booking.Id);
                if (bookingInDB != null) 
                {
                    return new JsonResult(NotFound());
                }

                bookingInDB = booking;
            }
            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }

        [HttpGet]
        public JsonResult GetOneBook(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(x => x.Id == id);
            if (booking == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(booking));
        }

        [HttpGet("/getAll")]
        public JsonResult getAll()
        {
            List<Booking> bookings = _context.Bookings.ToList();
            return new JsonResult(Ok(bookings));
        }

        [HttpPut]
        public JsonResult createBooking(Booking booking)
        {
            var bookingInDb = _context.Bookings.FirstOrDefault(x => x.Id == booking.Id);
            if (bookingInDb == null)
            {
                _context.Bookings.Add(booking);
                return new JsonResult(NotFound());
            }
            else
            {
                return new JsonResult(NoContent());
            }
        }

        [HttpDelete]
        public JsonResult deleteBook(int id)
        {
            var bookInDb = _context.Bookings.FirstOrDefault(x => x.Id == id);
            if (bookInDb == null)
            {
                
                return new JsonResult(NotFound());
            }
            else
            {
                _context.Bookings.Remove(bookInDb);
                return new JsonResult(NoContent());
            }
        }

    }
}
