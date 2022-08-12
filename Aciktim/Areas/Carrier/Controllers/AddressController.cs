using Microsoft.AspNetCore.Mvc;
using Aciktim.Models;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Aciktim.Areas.Carrier.Controllers
{
    [Authorize(Policy = "Carrier")]
    [Area("Carrier")]
    public class AddressController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            ViewBag.id = id;

            List<GetAddress> addresses = _context.GetCarrierAddress(id).ToList();
            
            List<Country> countries = new List<Country>();
            countries = (from Country in _context.Countries select Country).ToList();
            ViewBag.Country = countries;

            List<City> cities = new List<City>();
            cities = (from City in _context.Cities select City).ToList();
            ViewBag.City = cities;
            
            List<State> states = new List<State>();
            states = (from State in _context.States select State).ToList();
            ViewBag.State = states;

            List<Neighbourhood> neighbourhoods = new List<Neighbourhood>();
            neighbourhoods = (from Neighbourhood in _context.Neighbourhoods select Neighbourhood).ToList();
            ViewBag.Neighbourhood = neighbourhoods;

            List<Street> streets = new List<Street>();
            streets = (from Street in _context.Streets select Street).ToList();
            ViewBag.Street = streets;

            List<Apartment> apartments = new List<Apartment>();
            apartments = (from Apartment in _context.Apartments select Apartment).ToList();
            ViewBag.Apartment = apartments;

            List<ApartmentNumber> apartmentNumbers = new List<ApartmentNumber>();
            apartmentNumbers = (from ApartmentNumber in _context.ApartmentNumbers select ApartmentNumber).ToList();
            ViewBag.ApartmentNumber = apartmentNumbers;
            
            return View(addresses);
        }

        [HttpPost]
        public IActionResult Update(Address address)
        {
            if (address != null)
            {
                _context.Addresses.Update(address);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetCities(int id)
        {
            List<City> cities = new List<City>();
            cities = (from City in _context.Cities where City.CountryId == id select City).ToList();
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(cities, "CityId", "Name"));
        }
        public JsonResult GetStates(int id)
        {
            List<State> states = new List<State>();
            states = (from State in _context.States where State.CityId == id select State).ToList();
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(states, "StateId", "Name"));
        }
        public JsonResult GetNeighbourhoods(int id)
        {
            List<Neighbourhood> neighbourhoods = new List<Neighbourhood>();
            neighbourhoods = (from Neighbourhood in _context.Neighbourhoods where Neighbourhood.StateId == id select Neighbourhood).ToList();
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(neighbourhoods, "NeighbourhoodId", "Name"));
        }
        public JsonResult GetStreets(int id)
        {
            List<Street> streets = new List<Street>();
            streets = (from Street in _context.Streets where Street.NeighbourhoodId == id select Street).ToList();
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(streets, "StreetId", "Name"));
        }
        public JsonResult GetApartments(int id)
        {
            List<Apartment> apartments = new List<Apartment>();
            apartments = (from Apartment in _context.Apartments where Apartment.StreetId == id select Apartment).ToList();
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(apartments, "ApartmentId", "Name"));
        }
        public JsonResult GetApartmentNumbers(int id)
        {
            List<ApartmentNumber> apartmentNumbers = new List<ApartmentNumber>();
            apartmentNumbers = (from ApartmentNumber in _context.ApartmentNumbers where ApartmentNumber.ApartmentId == id select ApartmentNumber).ToList();
            return Json(new Microsoft.AspNetCore.Mvc.Rendering.SelectList(apartmentNumbers, "ApartmentNumberId", "Name"));
        }

    }
}
