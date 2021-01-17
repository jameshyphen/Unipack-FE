using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unipack.Models;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography.Core;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unipack.Views
{
    public sealed partial class MapPage : Page
    {
        public List<BasicGeoposition> Locations { get; set; } = new List<BasicGeoposition>();

        public MapPage()
        {
            this.InitializeComponent();
        }

        public MapPage(List<VacationLocation> locations):this()
        {
            RouteMap(locations);
        }

        private async void RouteMap(List<VacationLocation> locations)
        {
            Geolocator locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 1;

            foreach(VacationLocation l in locations)
            {
                if(l.CityName != null)
                    await Geocode(l.CityName);
                await Geocode(l.CountryName);
            }

            var path = new List<EnhancedWaypoint>();

            path.Add(new EnhancedWaypoint(new Geopoint(Locations.FirstOrDefault()), WaypointKind.Stop));
            Locations.ForEach(g => path.Add(new EnhancedWaypoint(new Geopoint(g), WaypointKind.Via)));
            path.Add(new EnhancedWaypoint(new Geopoint(Locations.LastOrDefault()), WaypointKind.Stop));

            MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteFromEnhancedWaypointsAsync(path);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Blue;
                viewOfRoute.OutlineColor = Colors.Black;

                Map.Routes.Add(viewOfRoute);

                await Map.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      MapAnimationKind.None);
            }
        }

        private async Task Geocode(string valueEntered)
        {

            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = 33.6667;
            location.Longitude = 73.1667;
            Geopoint hintPoint = new Geopoint(location);
            MapLocationFinderResult result =
                await MapLocationFinder.FindLocationsAsync(valueEntered, hintPoint);

            if (result.Status == MapLocationFinderStatus.Success)
            {
                try
                {
                    BasicGeoposition newLocation = new BasicGeoposition();
                    newLocation.Latitude = result.Locations.FirstOrDefault().Point.Position.Latitude;
                    newLocation.Longitude = result.Locations.FirstOrDefault().Point.Position.Longitude;
                    Locations.Add(newLocation);
                }catch(Exception ex)
                {

                }
            }
        }


    }
}


