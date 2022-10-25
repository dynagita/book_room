import Api from './ApiCommand';

export const post = async data =>{
    var url = 'user';

    var payload = {
        firstName: data.Name,
        lastName: data.LastName,
        email: data.Email,
        password: data.Password,
        bornDate: data.BornDate,
        reference: 0
    };

    return await Api.post(url, payload);
};