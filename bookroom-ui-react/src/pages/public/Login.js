import React, {useState, useContext, useReducer} from 'react';
import Css from '../../App.css';
import logo from '../../logo.svg';
import Grid from '@material-ui/core/Grid';
import TextBox from '../../component/TextBox';
import Button from '../../component/Button';
import {login, setUserData} from '../../services/Auth';
import {useUserContext} from '../../context/UserContext';
import { useHistory } from 'react-router-dom';
import {isAuthenticated} from '../../services/Auth';
import { Redirect } from 'react-router-dom';
import { useDialogContext } from '../../context/DialogContext';
import { useToastContext } from '../../context/ToastDialogContext';


export default function Login(){

    if(isAuthenticated())
    {
        return (<Redirect to={{ pathname: "/home" }} />);
    }else{
        return LoginView();
    }
}

function LoginView()
{
    const history = useHistory();

    const [state, dispatch] = useUserContext();

    const [values, setValues] = useState({
        Email: '',
        Password: ''        
      });

    const [EmailFailed, setEmailFailed] = useState({
        Error: false,
        Message: ''        
    });

    const [dialog, setDialogOpen, showModal] = useDialogContext();    

    const [toastState, dispatchToast, showToast] = useToastContext();

    const handleChange = prop => event => {           
        var value = event.target.value;
        setValues({ ...values, [prop]: value });
     };       

     function openModal(open)
     {
        showModal({load: true, show: open});
     }
     const handleClick = () =>{
        if(values.Email=== '' || values.Password === ''){
            showToast({type:'error', show: true, timems: 5000, title: 'Required Fields', message: 'Please, fill the required fields.'});
        }else{            
            openModal(true);
            var promisse = login(values);
            promisse.then(res => res.data)
            .then(payload => {
                let usr = {
                        
                        Email: payload.data.user.email,
                        Name: payload.data.user.firstName, 
                        LastName: payload.data.user.lastName,
                        BornDate: payload.data.user.bornDate,
                        Reference: payload.data.user.reference,
                        Token: payload.data.token
                };
                setUserData(usr);
                dispatch({Action: "updateUser", Payload: usr});
                openModal(false);
                history.push("/home");                                    
            })
            .catch(payload=>{
                showToast({type:'error', show: true, timems: 5000, title: 'Error', message: payload.response.data.error});
                openModal(false);
            });
        }
    }    

    return (
            <div className="App">
                <div className="Email">
                    <img src={logo} className="App-logo-mini" alt="logo" />
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <TextBox id="Email" label="Email" onChange={handleChange("Email")} value={values.Email} fullWidth error={EmailFailed.Error} required/>
                        </Grid>
                        <Grid item xs={12}>
                            <TextBox id="Password" label="Password" onChange={handleChange("Password")} value={values.Password} fullWidth type="password" helperText={EmailFailed.Message} error={EmailFailed.Error} required />
                        </Grid>
                        <Grid item xs={6}>
                            <Button onClick={handleClick} variant="contained" color="primary" fullWidth>
                                Sign In
                            </Button>
                        </Grid>
                        <Grid item xs={6}>
                            <Button variant="contained" color="secondary" href="/signup" fullWidth>
                                Sign up
                            </Button>
                        </Grid>                        
                    </Grid>
                </div>
            </div>
    );
}