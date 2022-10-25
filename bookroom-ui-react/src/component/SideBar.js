import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import User from '@material-ui/icons/Person';
import Clock from '@material-ui/icons/WatchLater';
import { useHistory } from 'react-router-dom';
import { Add } from '@material-ui/icons';

const useStyles = makeStyles({
  list: {
    width: 250,
  },  
});

export default function SideBar({toggleDrawer, open}) {
  const history = useHistory();
  const classes = useStyles();
  
  const sideList = side => (
    <div
      className={classes.list}
      role="presentation"
      onClick={toggleDrawer}
      onKeyDown={toggleDrawer}
    >
      <List>                  
          <ListItem button key="Insert Room" onClick={()=>history.push("/room")}>
            <ListItemIcon><Add /></ListItemIcon>
            <ListItemText primary="Insert Room" />
          </ListItem>
          <ListItem button key="Book a Room" onClick={()=>history.push("/bookroom")}>
            <ListItemIcon><Clock /></ListItemIcon>
            <ListItemText primary="Book a Room" />
          </ListItem>
          <ListItem button key="My Books" onClick={()=>history.push("/userbooks")}>
            <ListItemIcon><User /></ListItemIcon>
            <ListItemText primary="My Books" />
          </ListItem>
      </List>
      <Divider />      
    </div>
  );

  return (
    <div>
      <Drawer open={open} onClose={toggleDrawer('open',false)}>
        {sideList('open')}
      </Drawer>      
    </div>
  );
}
