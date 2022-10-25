import React, {useContext, createContext, useReducer} from "react";
import { userReducer } from "../reducer/public/reducer";
import {getUserData} from '../services/Auth';

export const UserContext = createContext();

const userStateInitialValue = getUserData();

export const UserProvider = props => {
    
    const [state, dispatch] = useReducer(userReducer, userStateInitialValue);
    
    return (

    <UserContext.Provider value={[state, dispatch]}>
        {props.children}
    </UserContext.Provider>
    );
}

export const useUserContext = () => useContext(UserContext);

