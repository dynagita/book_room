import React from 'react';
import {Alert, AlertTitle} from '@material-ui/lab';
import Snackbar from '@material-ui/core/Snackbar';
import { useToastContext } from '../context/ToastDialogContext';

export default function ToastDialog(){
    const [state, reducer, showToast] = useToastContext();
    
    return(
        <Snackbar open={state.Show} autoHideDuration={state.TimeMs}>
            <Alert severity={state.Type} variant="filled">
                <AlertTitle>{state.Title}</AlertTitle>
                {state.Message}
            </Alert>
        </Snackbar>
    );  
}