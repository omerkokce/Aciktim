using Aciktim.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Aciktim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        AciktimContext _context = new AciktimContext();

        public HomeController(ILogger<HomeController> logger)
        {

            _logger = logger;
        }

        public IActionResult Index()
        {
            var a = User.FindFirstValue(ClaimTypes.Role);
            switch (User.FindFirstValue(ClaimTypes.Role))
            {
                case "Client": return Redirect("/Client");
                case "Restaurant": return Redirect("/Restaurant");
                case "Carrier": return Redirect("/Carrier");
                default: break;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            List<Country> countries = new List<Country>();
            countries = (from Country in _context.Countries select Country).ToList();
            countries.Insert(0, new Country { CountryId = 0, Name = "Select" });

            return View(countries);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            Client client = _context.Clients.FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);

            if (client == null)
            {
                Restaurant restaurant = _context.Restaurants.FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);
                if (restaurant == null)
                {
                    Carrier carrier = _context.Carriers.FirstOrDefault(x => x.EMail == model.EMail && x.Password == model.Password);
                    if (carrier == null)
                    {
                        ViewBag.message = "Kullanıcı Adı veya şifre hatalı";
                        return View();
                    }
                    else
                    {
                        Role role = _context.Roles.FirstOrDefault(r => r.RoleId == carrier.RoleId);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, carrier.EMail),
                            new Claim("UserName", carrier.CarrierName),
                            new Claim(ClaimTypes.Role, role.RoleName),
                            new Claim("CarrierId",carrier.CarrierId.ToString())
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
                        return Redirect("/Carrier");
                    }
                }
                else
                {
                    Role role = _context.Roles.FirstOrDefault(r => r.RoleId == restaurant.RoleId);
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, restaurant.EMail),
                    new Claim("UserName", restaurant.RestaurantName),
                    new Claim(ClaimTypes.Role, role.RoleName),
                    new Claim("RestaurantId",restaurant.RestaurantId.ToString())
                };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return Redirect("/Restaurant");
                }
            }
            else
            {
                Role role = _context.Roles.FirstOrDefault(r => r.RoleId == client.RoleId);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, client.EMail),
                    new Claim("UserName", client.ClientName),
                    new Claim(ClaimTypes.Role, role.RoleName),
                    new Claim("ClientId",client.ClientId.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Redirect("/Client");
            }
        }

        [HttpPost]
        public IActionResult SignUpClient(Client _client)
        {
            if (_client != null)
            {
                _context.Clients.Add(_client);
                _context.SaveChanges();
                return Redirect("/Home/Login");
            }
            return RedirectToAction("SignUp");
        }
        [HttpPost]
        public IActionResult SignUpRestaurant(SignUpRestaurant _restaurant)
        {
            if (_restaurant != null)
            {
                Manager m = new Manager { ManagerName = _restaurant.ManagerName, Surname = _restaurant.Surname, Tcno = _restaurant.TCNo };
                Address a = new Address { AddressName = _restaurant.AddressName, CountryId = _restaurant.CountryId, CityId = _restaurant.CityId, StateId = _restaurant.StateId, NeighbourhoodId = _restaurant.NeighbourhoodId, StreetId = _restaurant.NeighbourhoodId, ApartmentId = _restaurant.ApartmentId, ApartmentNumberId = _restaurant.ApartmentNumberId };

                _context.Managers.Add(m);
                _context.Addresses.Add(a);
                _context.SaveChanges();

                Restaurant r = new Restaurant { AddressId = a.AddressId, ManagerId = m.ManagerId, RestaurantName = _restaurant.RestaurantName, EMail = _restaurant.EMail, Phone = _restaurant.Phone, Password = _restaurant.Password, RoleId = _restaurant.RoleId };
                _context.Restaurants.Add(r);
                _context.SaveChanges();

                return Redirect("/Home/Login");
            }
            return RedirectToAction("SignUp");
        }
        public IActionResult SignUpCarrier(SignUpCarrier _carrier)
        {
            if (_carrier != null)
            {
                Address a = new Address { AddressName = _carrier.AddressName, CountryId = _carrier.CountryId, CityId = _carrier.CityId, StateId = _carrier.StateId, NeighbourhoodId = _carrier.NeighbourhoodId, StreetId = _carrier.StreetId, ApartmentId = _carrier.ApartmentId, ApartmentNumberId = _carrier.ApartmentNumberId };

                _context.Addresses.Add(a);
                _context.SaveChanges();

                Carrier c = new Carrier { AddressId = a.AddressId, CarrierName = _carrier.CarrierName, EMail = _carrier.EMail, Phone = _carrier.Phone, Password = _carrier.Password, RoleId = _carrier.RoleId };
                _context.Carriers.Add(c);
                _context.SaveChanges();

                return Redirect("/Home/Login");
            }
            return RedirectToAction("SignUp");
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