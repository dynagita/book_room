import React, {useEffect} from "react";
import { BrowserRouter, Route, Switch, Redirect, useHistory } from "react-router-dom";
import NewUser from './pages/public/NewUser';
import Login from './pages/public/Login';
import NotFound from "./pages/public/NotFound";
import { isAuthenticated, getUserData } from "./services/Auth";
import Menu from "./component/Menu"
import Home from "./pages/home/Home";
import Room from "./pages/room/Room";
import BookRoom from "./pages/bookroom/BookRoom";
import UserBookRoom from "./pages/bookroom/UserBookRoom";

const PrivateRoute = ({ component: Component, ...rest }) => (    
  <Route
    {...rest}
    render={
      props =>{
        if(isAuthenticated())
        {
          return (
          <>
            <Menu />
            <Component {...props} />
          </>);
        }else
        {
          return (<Redirect to={{ pathname: "/" }} />);
        }                            
      }        
    }
  />
);  

const Routes = () => {       
    return (
    <BrowserRouter>
      <Switch>
        <Route path="/" exact={true} component={Login} />                     
        <Route path="/signup" exact={true} component={NewUser} />        
        <PrivateRoute path="/home" exact={true} component={Home} />
        <PrivateRoute path="/room" exact={true} component={Room} />
        <PrivateRoute path="/bookroom" exact={true} component={BookRoom} />
        <PrivateRoute path="/userbooks" exact={true} component={UserBookRoom} />
        <Route component={NotFound} />
      </Switch>
    </BrowserRouter>
  );
}
export default Routes;