import React, {useState} from 'react';
import PageHeader from '../../component/PageHeader';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';
import Button from '../../component/Button';
import { getByUserMail } from '../../services/BookRoomService';
import { useToastContext } from '../../context/ToastDialogContext';
import Hotel from '@material-ui/icons/Hotel';
import User from '@material-ui/icons/Person';
import { Box } from '@material-ui/core';
import { getUserData } from '../../services/Auth';
export default function UserBookRoom(){
  
  const initialState = [];
  
  const [state, setState] = useState(initialState);
  
  const [toastState, dispatchToast, showToast] = useToastContext();  

  const onLoad = () =>{
    if(state.length === 0){
        var user = getUserData()?.User;
        if(user){
            var promisse = getByUserMail(user.Email);
            promisse.then(payload => {
                setState(payload.data.data);
            })
            .catch(payload => {
                showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: payload.response.data.error});
            });
        }else{
            showToast({type:'error', show: true, timems: 5000, title: 'Required Fields', message: 'Sorry, we could not find your user. Please logout, do login again and try once more!Please, fill the required fields.'});
        }
    }
  };

  const getStatus = (enumVal) =>{
    if(enumVal === 0){
        return "Requested";
    } else if(enumVal === 1){
        return "Confirmed";
    }else{
        return "Canceled";
    }
  };
   return (
      <Container>
          <PageHeader text="User Book" icon={<User />}/>          
          <Grid container spacing={3} style={{fontWeight: 600}}>
                <Grid item xs={12} align='right'>
                        <Button variant="contained" color="primary" onClick={onLoad}>
                        <Hotel />  Show Books
                        </Button> 
                </Grid>                
           </Grid>
            <Box display={state.length > 0 ? "block" : "none"}>
                <Grid container spacing={3} style={{fontWeight: 600}}>
                        <Grid item xs={2}>Room Number</Grid>
                        <Grid item xs={3}>Start Date</Grid>
                        <Grid item xs={3}>EndDate</Grid>
                        <Grid item xs={3}>Status</Grid>
                    </Grid>
                {                        
                    state.map((book) => 
                    <Grid container spacing={3}>
                        <Grid item xs={2}>{book.room.number}</Grid>
                        <Grid item xs={3}>{book.startDate}</Grid>
                        <Grid item xs={3}>{book.endDate}</Grid>
                        <Grid item xs={3}>{getStatus(book.status)}</Grid>
                    </Grid>
                    )
                }                
          </Box>
        </Container>
  )
}