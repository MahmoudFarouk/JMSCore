
export interface Checkpoint {
    id: number;
    name: string;
    latitude?: number;
    longitude?: number;
    isThirdParty: boolean;
    isFromOrTo:boolean;
    isDeleted:boolean;
}
