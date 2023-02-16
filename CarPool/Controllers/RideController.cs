using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace CarPool.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/Ride")]
    public class RideController : Controller
    {
        IBookingService bookingService;
        IOfferingService offeringService;
        public RideController(IBookingService bookingService, IOfferingService offeringService)
        {
            this.bookingService = bookingService;
            this.offeringService = offeringService;
        }
        [HttpPost]
        [Route("Offer")]
        public void OfferRide(RideDetails ride)
        {


            offeringService.OfferRide(ride);
        }

        [HttpPost]
        [Route("Matching")]
        public IActionResult GetMatchingRides(RideDetails ride)
        {
            try
            {
                return Ok(bookingService.GetMatchingRides(ride));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("Book")]
        public IActionResult BookRide(RideDetails ride)
        {
/*            try
            {*/
                bookingService.BookRide(ride);
                return Ok(ride);

/*            }
            catch
            {
                return BadRequest();
            }*/

        }

        [HttpGet]
        [Route("Booked")]
        public IActionResult GetBookedRides()
        {

            return Ok(bookingService.GetBookedRides());
        }

        [HttpGet]
        [Route("Offered")]
        public IActionResult GetOfferedRides()
        {
            return Ok(offeringService.GetOfferedRides());
        }
    }
}
