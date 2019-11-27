import swal from "sweetalert2";
import { throwError } from 'rxjs';
export class General{
    public static errorHandl(error) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            errorMessage = error.error.message;
        } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }

        swal.fire("Error", "Sorry an error occured, Please try again or contact administration.", "error");
        console.log(errorMessage);
        return throwError(errorMessage);
    }
}