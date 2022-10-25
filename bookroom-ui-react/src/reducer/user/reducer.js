export const reducer = (state, action) =>{
    switch(action.Type){
        case 'password':
            return({
                ...state,
                Password: action.Payload
            });
        case 'confirmpassword':
            return({
                ...state,
                ConfirmPassword: action.Payload
            });
        case 'passworderror':
            return({
                ...state,
                PasswordError: action.Payload
            });
        case 'showload':
            return({...state, ShowLoad: action.Payload});
        default:
            return state;
    }
}

export const userScreenReducer = (state, action) =>{
    return({...state, [action.Prop]: action.Payload});
}