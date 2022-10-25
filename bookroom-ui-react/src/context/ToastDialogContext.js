import React, {useContext, createContext, useReducer} from "react";
import { reducerToast } from "../reducer/public/reducer";

export const ToastContext = createContext();

const initialState = {
        Type: '',
        Show: false,
        TimeMs: 5000,
        Title: '',
        Message: ''
};

export const ToastProvider = props =>{
    
    const [state, dispatch] = useReducer(reducerToast, initialState);
    
    function showToast({type, show, timems, title, message, closeCallback}){
        dispatch({Type: 'type', Payload: type});
        dispatch({Type: 'show', Payload: show});
        dispatch({Type: 'timems', Payload: timems});
        dispatch({Type: 'title', Payload: title});
        dispatch({Type: 'message', Payload: message});

        setTimeout(function(){
            
            dispatch({Type: 'show', Payload: false});

            if(closeCallback !== null && closeCallback !== undefined)
                closeCallback();
        }, timems);

    }

    return(
        <ToastContext.Provider value={[state, dispatch, showToast]}>
            {props.children}
        </ToastContext.Provider>
    );
}

export const useToastContext = () => useContext(ToastContext);