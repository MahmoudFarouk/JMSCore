

// export class User {
//     id: number;
//     username: string;
//     password: string;
//     firstName: string;
//     lastName: string;
//     role: Role;
//     token?: string;
//     fullName:string;
// }

    export interface Role {
        id: string;
        name: string;
    }

    export interface User {
        id: string;
        username: string;
        fullName: string;
        userGroupId?: any;
        userWorkForceId?: any;
        licenseNo?: any;
        licenseExpiryDate?: any;
        trainingDetails?: any;
        gatePassStatus?: any;
        token: string;
        roles: Role[];
    }





