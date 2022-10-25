import React from 'react';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { useDialogContext } from '../context/DialogContext';
import Load from './Load';

export default function AlertDialog() {    
    
    const [state, dispatch] = useDialogContext();

    function showBody()
    {
        if(state.LoadModal){
            return <Load />;
        }else{
            return state.Body;
        }
    }

    function ConfigModal (){
        return(
            <Dialog
                open={state.Open}
                onClose={state.OnClose}
                fullScreen
            >
                <DialogTitle id="alert-dialog-title">{state.Title}</DialogTitle>
                <DialogContent style={{minWidth: "60vh", minHeight: "15vh"}}>
                    {showBody()}
                </DialogContent>                
            </Dialog>
        );
    };

    return ConfigModal();
}