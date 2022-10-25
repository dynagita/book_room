export const emailIsValid = (value, dispatch) => {
    let result = false;
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(value))
    {      
        result = true;
    }

    let message = "";

    if(!result && value !== "")
        message = "E-mail invalid.";
    
    dispatch({Type:"emailerror", Payload: message});

    return result;
    
}

export const passwordIsValid = (password, confirmPassword, dispatch)=>
{
    let result = true;
    if(password !== confirmPassword)
    {
        result = false;
    }
    
    let message = "";

    if(!result && confirmPassword !== "")
        message = "Password does not match";

    dispatch({Type:"passworderror", Payload: message});

    return result;
}    