export const reducer = (state, action )=>{
    switch(action.Type)
    {
      case "name":
        return (
          {
            ...state,
            Name: action.Payload
          }
        )
      case "lastname":
        return(
          {
            ...state,
            LastName: action.Payload
          }
        )        
      case "email":
          return(
            {
              ...state,
              Email: action.Payload
            }
          )
      case "borndate":
          return(
              {
                ...state,
                BornDate: action.Payload
              }
            )
      case "password":
        return(
            {
              ...state,
              Password: action.Payload
            }
          )
      case "confirmpassword":
        return(
            {
              ...state,
              ConfirmPassword: action.Payload
            }
          )
      case "passworderror":
        return(
            {
              ...state,
              PasswordError: action.Payload
            }
          )
      case "emailerror":
        return(
            {
              ...state,
              EmailError: action.Payload
            }
          )
      case "dialog":
        return(
          {
            ...state,
            DialogOpen: action.Payload
          }
        )      
      default:
        return state;
    }
  }

  export const userReducer = (state, action) =>{
    switch(action.Action)
    {
      case 'updateUser':
        return{...state,
          User: action.Payload        
        };
      default:
        return state;
    }
  }

 export const reducerDialog = (state, action) => {
    switch(action.Type){
        case 'modal':
            return ({...state, Open: action.Payload});
        case 'title':
            return ({...state, Title: action.Payload});
        case 'body':
          return ({...state, Body: action.Payload});
        case 'load':
          return ({...state, LoadModal: action.Payload});
        case 'onclose':
          return ({...state, OnClose: action.Payload});
        default:
            return state;
    }
}

export const reducerToast = (state, action)=>{
  switch(action.Type){    
    case 'type':
        return({...state, Type: action.Payload});
    case 'show':
      return({...state, Show: action.Payload});        
    case 'timems':
      return({...state, TimeMs: action.Payload});
    case 'title':
      return({...state, Title: action.Payload});
    case 'message':
      return({...state, Message: action.Payload});
    default:
      return state;
  }
}