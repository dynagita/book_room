import Api from './ApiQuery';

const userStateInitialValue = {
  User: {
      Email: "",
      Name: "",
      LastName: "",
      Token: ""
  }
};

export const USR_DATA_KEY = "BookRoomData";

export const isAuthenticated = () => {
  let data = getUserData();
  return data.User.Token !== "";
}
export const getToken = () => {
  let data = getUserData();
  return data.User.Token;
}
export const login = async data => {
  var url = "Authentication"
  data.Token = '';

  return await Api.post(url, data);
};
export const logout = () => {
  localStorage.removeItem(USR_DATA_KEY);
  window.location.reload();
};

export const setUserData = (usr) =>
{
  let data = {User: usr};
  localStorage.setItem(USR_DATA_KEY, JSON.stringify(data));
}

export const getUserData = () =>{
  let usr = localStorage.getItem(USR_DATA_KEY);
  if(usr === null || usr === undefined)
    return userStateInitialValue;
  
  return JSON.parse(usr);
}

