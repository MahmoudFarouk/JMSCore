import { ResponseStatus } from '../Enums/response-status.enum';

export interface ServiceResponse<T>{
    status: ResponseStatus,
    message: string,
    exception: string,
    data: T
}