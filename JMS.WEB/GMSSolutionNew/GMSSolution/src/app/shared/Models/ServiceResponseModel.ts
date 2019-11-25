export interface ServiceResponse<T>{
    status: number,
    message: string,
    exception: string,
    data: T
}
