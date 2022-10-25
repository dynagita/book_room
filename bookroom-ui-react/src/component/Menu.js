import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import SideBar from './SideBar'
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import {logout} from "../services/Auth";
import { CenterFocusStrongOutlined } from '@material-ui/icons';
import AccountBoxOutlinedIcon from '@material-ui/icons/AccountBoxOutlined';
import Tooltip from '@material-ui/core/Tooltip';
import {useHistory, Link} from 'react-router-dom';
import{useUserContext} from '../context/UserContext';

const useStyles = makeStyles(theme => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
    textAlign: "center",
    
  },
  linkNoDecoration:{
    textDecoration: "none",
    flexGrow: 1,
    textAlign: "center",
    color: "white"
  }
}));

export default function Menu() {

  const [userState, dispatch] = useUserContext();

  let userName = userState.User.Name+" "+userState.User.LastName;

  const history = useHistory();

  const classes = useStyles();  

  const [state, setState] = React.useState({
    open: false
  });

  useEffect(()=>{
    return function(){
      if(state.open)
        setState({...state, ["open"]: false});
    };
  });

  const toggleDrawer = (side, open) => event => {
    if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }

    setState({ ...state, [side]: open });
  };

  return (
    <div className={classes.root}>
      <SideBar toggleDrawer={toggleDrawer} open={state.open}/>
      <AppBar position="static">
        <Toolbar>
          <IconButton onClick={toggleDrawer('open',true)} edge="start" className={classes.menuButton} color="inherit" aria-label="menu">
            <MenuIcon />
          </IconButton>
          <Link to="/home" className={classes.linkNoDecoration} color="inherit">
            <Typography variant="h6">
              Book Room
            </Typography>
            </Link>
          
          <div>       
            <Tooltip title={userName} placement="bottom-start" >
            <IconButton className={classes.menuButton} color="inherit" aria-label="user">
                  <AccountBoxOutlinedIcon />
              </IconButton>
            </Tooltip>     
            <Tooltip title="logout" placement="bottom-start">
              <IconButton onClick={logout} edge="start" className={classes.menuButton} color="inherit" aria-label="logout">
                <ExitToAppIcon />
              </IconButton>
            </Tooltip>            
          </div>
        </Toolbar>
      </AppBar>
    </div>
  );
}
