import React, {useReducer} from 'react';
import PageHeader from '../../component/PageHeader';
import Container from '@material-ui/core/Container';
import UserIcon from '@material-ui/icons/AccountBox';
import Grid from '@material-ui/core/Grid';
import TextBox from '../../component/TextBox';
import Button from '../../component/Button';
import DateTimePicker from '../../component/DateTimePicker';
import {post} from '../../services/UserService';
import {useHistory} from 'react-router-dom';
import {passwordIsValid,emailIsValid} from '../../rules/RegisterRules';
import {reducer} from "../../reducer/public/reducer";
import { useDialogContext } from '../../context/DialogContext';
import { useToastContext } from '../../context/ToastDialogContext';

export default function NewUser()
{
  const history = useHistory();

  const [dialog, dispatchDialog] = useDialogContext();
  
  const [state, dispatch] = useReducer(reducer,
                                        {
                                          Name: '',
                                          LastName: '',
                                          Email: '',
                                          BornDate: new Date(1986,1,1),
                                          Password: '',
                                          ConfirmPassword: '',
                                          PasswordError: '',
                                          EmailError: ''
                                        });
    
     const [toastState, dispatchToast, showToast] = useToastContext();
     
     const handleSuccessRegister = ()=>{
        history.push("/");
    }

    const handleChange = prop => event => {           
      let value = event.target.value;
      
      if(prop === "confirmpassword"){
        passwordIsValid(state.Password, value, dispatch);
      }
      else if(prop === "email")
      {
        emailIsValid(value, dispatch);
      }

      dispatch({Type: prop, Payload: value});
   };     
   
    const handleDateChange = date => {
      dispatch({Type: "borndate", Payload: date});      
    };

    const goBackToLogin = () =>
    {
      history.push("/");
    }

    function showModal(open)
    {
      dispatchDialog({Type:'load', Payload: true});
      dispatchDialog({Type:'modal', Payload: open});      
    }

    const handleClick = () =>{

      var date = new Date().getDate();
      let passwordValid = passwordIsValid(state.Password, state.ConfirmPassword, dispatch);
      let emailValid = emailIsValid(state.Email, dispatch);
      if(state.Name === '' || state.LastName === '' || state.Email === '' ){        
        showToast({type: "error", show: true, timems: 5000, title: 'Error', message: 'Required fields must be filled.'});
        //type, show, timems, title, message,handleclose
      }
      else if (!passwordValid){
        showToast({type: "error", show: true, timems: 5000, title: 'Error', message: 'Password and Confirm Password does not match. Please, correct this mistake and try again.'});
      }
      else if(!emailValid){
        showToast({type: "error", show: true, timems: 5000, title: 'Error' , message: 'Please, insert a valid e-mail.'});
      }
      else{
        showModal(true);
        var promisse = post(state);
        promisse.then(res => res.data)
        .then(data => {
          showModal(false);            
          showToast({type: 'success', show: true, timems: 3000, title: 'Success', message: 'Congratulations! User created successfully.', closeCallback: handleSuccessRegister});            
        })
        .catch(payload => {
          showModal(false);
          showToast({type: 'error', show: true, timems: 5000, title: 'Error', message: payload.response.data.error});
        });
      }
    }
    return (
        <Container>
          <PageHeader text="New User" icon={<UserIcon />}/>          
          <Grid container spacing={3}>
              <Grid item xs={6} >          
                <TextBox id="Name" label="Name" onChange={handleChange("name")} value={state.Name} fullWidth required error={state.Name === ""}/>
              </Grid>
              <Grid item xs={6} >          
                <TextBox id="LastName" label="Last Name" onChange={handleChange("lastname")} value={state.LastName} fullWidth required error={state.LastName === ""}/>
              </Grid>            
              <Grid item xs={6}>
                <TextBox id="Email" label="E-mail" onChange={handleChange("email")} value={state.Email} fullWidth helperText={state.EmailError} error={state.EmailError!=="" || state.Email === ""} required />
              </Grid>
              <Grid item xs={6} >          
                <DateTimePicker value={state.BornDate} handleDateChange = {handleDateChange} type="Date" label="Born Date" required={true} />
              </Grid>  
              <Grid item xs={12} >
                <TextBox id="Password" label="Password" onChange={handleChange("password")} value={state.Password} fullWidth type="password" required error={state.Password === ""}/>
              </Grid>  
              <Grid item xs={12} >              
                <TextBox id="ConfirmPassword" label="Confirm Password" onChange={handleChange("confirmpassword")} value={state.ConfirmPassword} fullWidth type="password" helperText={state.PasswordError} error={state.PasswordError!==""} required/>
              </Grid>  
              <Grid item xs={12}  align='right'>
                <Button variant="contained" color="primary" onClick={goBackToLogin}>
                  Login
                </Button> 
                {"  "}
                <Button variant="contained" color="secondary" onClick={handleClick}>
                  Register
                </Button>
              </Grid>              
            </Grid>  
        </Container>
    );
    //<Button variant="contained" color="primary" href="/" onClick={()=>{console.log(values)}}>
}