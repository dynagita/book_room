import Command from './ApiCommand';
import Query from './ApiQuery';

export const post = async data =>{
    var url = "BookRoom";
    return await Command.post(url, data);
};

export const checkAvailability = async (data) => {
    var url = "BookRoom/availability"
    return await Query.get(url,{params: {startDate: data.startDate, endDate: data.endDate, roomNumber: data.roomNumber}});
}

export const getByUserMail = async (mail) =>{
    var url = "BookRoom"
    return await Query.get(url,{params: {email: mail}});
}