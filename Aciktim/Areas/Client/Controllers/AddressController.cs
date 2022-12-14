using Aciktim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Security.Claims;

namespace Aciktim.Areas.Client.Controllers
{
    [Authorize(Policy = "Client")]
    [Area("Client")]
    public class AddressController : Controller
    {
        AciktimContext _context = new AciktimContext();
        public IActionResult Index(int id)
        {
            string name = User.FindFirstValue("UserName");
            ViewBag.name = name;
            dynamic myModal = new ExpandoObject();
            myModal.Address = _context.GetClientFullAddress(id).ToList();

            List<Country> countries = new List<Country>();
            countries = (from Country in _context.Countries select Country).ToList();
            countries.Insert(0, new Country { CountryId = 0, Name = "Select" });
            myModal.Country = countries;

            ViewBag.id = id;

            return View(myModal);
        }

        [Route("/Client/Address/Delete/{addressId}/{clientId}")]
        public IActionResult Delete(int addressId, int clientId)
        {
            ClientAddress a = _context.ClientAddresses.FirstOrDefault(a => a.AddressId == addressId && a.ClientId == clientId);
            if (a != null)
            {
                _context.ClientAddresses.Remove(a);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = clientId });
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("/Client/Address/Add/{clientId}")]
        [HttpPost]
        public IActionResult Add(Address a, int clientId)
        {
            try
            {
                _context.Addresses.Add(a);
                _context.SaveChanges();
                _context.ClientAddresses.Add(new ClientAddress { AddressId = a.AddressId, ClientId = clientId });
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction("Index", new { id = clientId });
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
