using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using System.Dynamic;

namespace Aciktim.Areas.Carrier.Controllers
{
    [Area("Carrier")]
    public class AddressController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index()
        {
            dynamic myModal = new ExpandoObject();
            myModal.Address = _context.GetCarrierAddress(5).ToList();

            List<Country> countries = new List<Country>();
            countries = (from Country in _context.Countries select Country).ToList();
            countries.Insert(0, new Country { CountryId = 0, Name = "Select" });
            myModal.Country = countries;

            return View(myModal);
        }

        public JsonResult GetCities(int id)
        {
            List<City> cities = new List<City>();
            cities = (from City in _context.Cities where City.CountryId == id select City).ToList();
            cities.Insert(0, new City { CityId = 0, Name = "Select" });
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(cities, "CityId", "Name"));
        }
        public JsonResult GetStates(int id)
        {
            List<State> states = new List<State>();
            states = (from State in _context.States where State.CityId == id select State).ToList();
            states.Insert(0, new State { StateId = 0, Name = "Select" });
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(states, "StateId", "Name"));
        }
        public JsonResult GetNeighbourhoods(int id)
        {
            List<Neighbourhood> neighbourhoods = new List<Neighbourhood>();
            neighbourhoods = (from Neighbourhood in _context.Neighbourhoods where Neighbourhood.StateId == id select Neighbourhood).ToList();
            neighbourhoods.Insert(0, new Neighbourhood { NeighbourhoodId = 0, Name = "Select" });
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(neighbourhoods, "NeighbourhoodId", "Name"));
        }
        public JsonResult GetStreets(int id)
        {
            List<Street> streets = new List<Street>();
            streets = (from Street in _context.Streets where Street.NeighbourhoodId == id select Street).ToList();
            streets.Insert(0, new Street { StreetId = 0, Name = "Select" });
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(streets, "StreetId", "Name"));
        }
        public JsonResult GetApartments(int id)
        {
            List<Apartment> apartments = new List<Apartment>();
            apartments = (from Apartment in _context.Apartments where Apartment.StreetId == id select Apartment).ToList();
            apartments.Insert(0, new Apartment { ApartmentId = 0, Name = "Select" });
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(apartments, "ApartmentId", "Name"));
        }
        public JsonResult GetApartmentNumbers(int id)
        {
            List<ApartmentNumber> apartmentNumbers = new List<ApartmentNumber>();
            apartmentNumbers = (from ApartmentNumber in _context.ApartmentNumbers where ApartmentNumber.ApartmentId == id select ApartmentNumber).ToList();
            apartmentNumbers.Insert(0, new ApartmentNumber { ApartmentNumberId = 0, Name = "Select" });
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(apartmentNumbers, "ApartmentNumberId", "Name"));
        }

    }
}
