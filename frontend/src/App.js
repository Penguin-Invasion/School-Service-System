import { useState } from 'react';
import { HashRouter, Route, Switch, Redirect } from "react-router-dom";

import "assets/plugins/nucleo/css/nucleo.css";
import "@fortawesome/fontawesome-free/css/all.min.css";
import "assets/scss/argon-dashboard-react.scss";

import AdminLayout from "layouts/Admin.js";
import AuthLayout from "layouts/Auth.js";

import useToken from './useToken';

// take token from local storage and check if undefined, clear the local storage
// if token is not undefined, set the token to the state
// if token is undefined, redirect to login page
const checkToken = () => {
    const tokenString = localStorage.getItem('token');
    const token = JSON.parse(tokenString);
    if (token === null || token === undefined) {
        localStorage.clear();
    }
}
      
const App = () => {

    //checkToken();
          
    const { token, setToken } = useToken();


    // if user not logged in, redirect to login page
    if (!token) {
        return (
            <HashRouter>
                <Switch>
                    {/* <Route path="/admin" component={AdminLayout} /> */}
                    {/* send props to AuthLayout */}
                    <Route path="/auth" render={props => <AuthLayout {...props} setToken={setToken} />} />
                    {/* <Route path="/auth" component={AuthLayout} foo={"bar"} /> */}
                    <Redirect from="/" to="/auth/login" />
                </Switch>
            </HashRouter>
        );
    }


    return (
        <HashRouter>
            <Switch>
            <Route path="/admin" render={(props) => <AdminLayout {...props} setToken={setToken} />} />
            {/* <Route path="/auth" render={(props) => <AuthLayout {...props} />} /> */}
            <Redirect from="/" to="/admin/index" />
            </Switch>
        </HashRouter>
    )
}

export default App
