import React, {useState} from 'react';
import PageHeader from '../../component/PageHeader';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';
import TextBox from '../../component/TextBox';
import Button from '../../component/Button';
import {post} from '../../services/RoomService';
import {useHistory} from 'react-router-dom';
import { useDialogContext } from '../../context/DialogContext';
import { useToastContext } from '../../context/ToastDialogContext';
import { Add } from '@material-ui/icons';

export default function Room(){
  
  const initialState = {
    reference: 0,
    title: "",
    description: "",
    number: 0
  };

  const [state,setState] = useState(initialState);
  const history = useHistory();

  const [dialog, dispatchDialog] = useDialogContext();

  const [toastState, dispatchToast, showToast] = useToastContext();

  const handleChange = prop => event => {           
    let value = event.target.value;
    if(prop === "number"){
        value = value.replace(/[^0-9]/g, '');

    }
    setState({...state, [prop]: value});
 };  

 const resetState = () => {
    setState(initialState);
 };

 const onAdd = () =>{
    var promisse = post(state);
    promisse.then(data => {
        showToast({type: 'success', show: true, timems: 3000, title: 'Success', message: 'Congratulations! Room created successfully.', closeCallback: resetState});            
    })
    .catch(payload => {
        showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: payload.response.data.error});
    });
 };

  return (
      <Container>
          <PageHeader text="New Room" icon={<Add />}/>          
          <Grid container spacing={3}>
              <Grid item xs={12} >          
                <TextBox id="Title" label="Title" onChange={handleChange("title")} value={state.title} fullWidth required error={state.title === ""}/>
              </Grid>
              <Grid item xs={12} >          
                <TextBox id="Description" label="Description" onChange={handleChange("description")} value={state.description} fullWidth required error={state.description === ""}/>
              </Grid>            
              <Grid item xs={4}>
                <TextBox id="Number" label="Number" onChange={handleChange("number")} value={state.number} fullWidth error={state.number <= 0} required />
              </Grid>
              <Grid item xs={6} >          
              </Grid>  
              <Grid item xs={12}  align='right'>
                <Button variant="contained" color="primary" onClick={onAdd}>
                  <Add></Add> Insert
                </Button> 
              </Grid>              
            </Grid>  
        </Container>
  )
}