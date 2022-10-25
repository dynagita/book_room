import React, {useState} from 'react';
import PageHeader from '../../component/PageHeader';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';
import TextBox from '../../component/TextBox';
import Button from '../../component/Button';
import { get } from '../../services/RoomService';
import { checkAvailability, post } from '../../services/BookRoomService';
import { useHistory } from 'react-router-dom';
import { useDialogContext } from '../../context/DialogContext';
import { useToastContext } from '../../context/ToastDialogContext';
import Clock from '@material-ui/icons/WatchLater';
import Hotel from '@material-ui/icons/Hotel';
import Check from '@material-ui/icons/Check';
import Add from '@material-ui/icons/Add';
import Close from '@material-ui/icons/Close';
import DateTimePicker from '../../component/DateTimePicker';
import { Box } from '@material-ui/core';
import { getUserData } from '../../services/Auth';
export default function BookRoom(){
  
  const initialState = [];
  
  const onLoad = () =>{
    if(state.length === 0){
        var promisse = get();
        promisse.then(payload => {
            setState(payload.data.data);
        })
        .catch(payload => {
            showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: payload.response.data.error});
        });
    }
  };

  const [state,setState] = useState(initialState);

  const[disponibility, setDisponibility] = useState({
    startDate: new Date(),
    endDate: new Date(),
    roomNumber: 0
  });

  const history = useHistory();

  const [dialog, dispatchDialog, showModal] = useDialogContext();

  const [toastState, dispatchToast, showToast] = useToastContext();  

  const resetState = () => {
    setState(initialState);
  };

  const onBook = () =>{    
  };

  const handleStartDateChange = date => {
    setDisponibility({...disponibility, startDate: date})    
  };

  const handleEndDateChange = date => {
    setDisponibility({...disponibility, endDate: date})    
  };

  const handleChange = event => {               
    let value = event.target.value;
    value = value.replace(/[^0-9]/g, '');
    setDisponibility({...disponibility, roomNumber: value});
  };  

  const onCheckAvailability = () => {
    if(disponibility.endDate < disponibility.startDate){
        showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: "End date must be greater than start date."});
        return;
    }
    if(!state.some(x => x.number === parseInt(disponibility.roomNumber))){
        showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: "Select a room listed above."});
        return;
    }
    let promisse = checkAvailability(disponibility);
    promisse.then(payload =>{
        if(payload.data.data.available){
            showToast({type: 'success', show: true, timems: 3000, title: 'Success', message: "Room available! Go ahead and book a room at the best Cancun's Hotel."});
        }else{
            let title = payload.data.data.unavailableMessage;
            
            showModal({title: <Grid container spacing={3} style={{fontWeight: 600}}>                    
                <Grid item xs={12} align="right"><Close onClick={() => showModal({open: false})}/></Grid>
                <Grid item xs={12}> {title} </Grid>
                <Grid item xs={3}> {"Room: "+payload.data.data.room.number} </Grid>                
            </Grid>, load: false, body: 
            <Box>                
                <Grid container spacing={3} style={{fontWeight: 600}}>                    
                    <Grid item xs={6}>Start Date</Grid>           
                    <Grid item xs={6}>End Date</Grid>
                </Grid>
                {payload.data.data.room.books.map((book) => 
                    <Grid container spacing={3}>
                        <Grid item xs={6}>{book.startDate}</Grid>
                        <Grid item xs={6}>{book.endDate}</Grid>               
                    </Grid>
                    )
                }               
            </Box>, show: true, });
        }
    })
    .catch(payload => {
        showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: payload.response.data.error});
    });
  }

  const getPayload = () => {
    var user = getUserData()?.User;
    if(user === null || user === undefined){
        showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: "Sorry, we could not find your user. Please logout, do login again and try once more!"});
        return;
    }
    var room = state.find(x => x.number === parseInt(disponibility.roomNumber));
    if(room === null || room === undefined){
        showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: "Select a room listed above."});
        return;
    }

    let payload = {
        reference: 0,
        startDate: disponibility.startDate,
        endDate: disponibility.endDate,
        status: 0,
        user: {
          firstName: user.Name,
          lastName: user.LastName,
          bornDate: user.BornDate,
          email: user.Email,
          reference: user.Reference
        },
        room: {
          reference: room.id,
          title: room.title,
          description: room.description,
          number: room.number
        }
      };

      return payload;
  }

  const onBookRoom = () =>{

    var data = getPayload();
    if(data){
        var promisse = post(data);
        promisse.then(payload => {
            showToast({type: 'success', show: true, timems: 10000, title: 'Success', message: "Your book has been sent and will be processed. Take a look at your book's status on page 'My Books'!"});
        })
        .catch(payload => {
            showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: payload.response.data.error});
        });
    }
      
  }

   return (
      <Container>
          <PageHeader text="Book Room" icon={<Clock />}/>          
          <Grid container spacing={3} style={{fontWeight: 600}}>
                <Grid item xs={12} align='right'>
                        <Button variant="contained" color="primary" onClick={onLoad}>
                        <Hotel />  Show Rooms
                        </Button> 
                </Grid>                
           </Grid>
            <Box display={state.length > 0 ? "block" : "none"}>
                <Grid container spacing={3} style={{fontWeight: 600}}>
                        <Grid item xs={2}>Room Number</Grid>           
                        <Grid item xs={4}>Title</Grid>
                        <Grid item xs={6}>Description</Grid>                        
                    </Grid>
                {                        
                    state.map((room) => 
                    <Grid container spacing={3}>
                    <Grid item xs={2}>{room.number}</Grid>    
                    <Grid item xs={4}>{room.title}</Grid>
                    <Grid item xs={6}>{room.description}</Grid>               
                    </Grid>
                    )
                }
                <br /><br /><br />
                <Grid container spacing={3} style={{fontWeight: 600}}>
                    <Grid item xs={2}><TextBox id="RoomNumber" label="Room Number" onChange={handleChange} value={disponibility.roomNumber} fullWidth error={disponibility.roomNumber <= 0} required /></Grid>
                    <Grid item xs={2}><DateTimePicker value={disponibility.startDate} handleDateChange={handleStartDateChange} type="Date" required={true} /></Grid>
                    <Grid item xs={2}><DateTimePicker value={disponibility.endDate} handleDateChange={handleEndDateChange} type="Date" required={true} /></Grid>
                    <Grid item xs={2}><Button color="primary" onClick={onCheckAvailability}><Check /> Available?</Button></Grid>
                    <Grid item xs={2}><Button color="primary" onClick={onBookRoom}><Add /> Book this Room</Button></Grid>
                </Grid>
          </Box>
        </Container>
  )
}