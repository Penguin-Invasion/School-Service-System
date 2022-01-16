import Index from "views/Index.js";
import Profile from "views/examples/Profile.js";
import Login from "views/examples/Login.js";
import Tables from "views/examples/Tables.js";
import EditProfile from "views/examples/editProfile.js";
import AddService from "views/examples/addService.js";
import AddDriver from "views/examples/addDriver.js";
var routes = [
  {
    path: "/index",
    show: true,
    name: "Dashboard",
    icon: "ni ni-tv-2 text-primary",
    component: Index,
    layout: "/admin",
  },
  {
    path: "/user-profile",
    show: true,
    name: "User Profile",
    icon: "ni ni-single-02 text-yellow",
    component: Profile,
    layout: "/admin",
  },
  {
    path: "/tables",
    show: true,
    name: "Services",
    icon: "ni ni-bullet-list-67 text-red",
    component: Tables,
    layout: "/admin",
  },
  {
    path: "/login",
    show: false,
    name: "Login",
    icon: "ni ni-key-25 text-info",
    component: Login,
    layout: "/auth",
  },
  {
    path: "/edit-profile",
    show: false,
    name: "Edit Profile",
    icon: "ni ni-key-25 text-info",
    component: EditProfile,
    layout: "/admin",
  },
  {
    path: "/add-service",
    show: false,
    name: "Edit Service",
    icon: "ni ni-key-25 text-info",
    component: AddService,
    layout: "/admin",
  },
  ,
  {
    path: "/add-Driver",
    show: false,
    name: "Edit Driver",
    icon: "ni ni-key-25 text-info",
    component: AddDriver,
    layout: "/admin",
  },

];
export default routes;
