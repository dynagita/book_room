import React, {useContext, createContext, useReducer} from "react";
import { reducerDialog } from "../reducer/public/reducer";

export const DialogContext = createContext();

const initialState = {
    Open: false,
    Title: '',
    Body: null,
    LoadModal: true,
    OnClose: null
};

export const DialogProvider = props =>{
    
    const [state, dispatch] = useReducer(reducerDialog, initialState);

    function showModal({title, load, body, show}){       
        dispatch({Type: 'title', Payload: title});
        dispatch({Type: 'load', Payload: load});
        dispatch({Type: 'body', Payload: body});
        dispatch({Type: 'modal', Payload: show});
    }

    return(
        <DialogContext.Provider value={[state, dispatch, showModal]}>
            {props.children}
        </DialogContext.Provider>
    );
}

export const useDialogContext = () => useContext(DialogContext);