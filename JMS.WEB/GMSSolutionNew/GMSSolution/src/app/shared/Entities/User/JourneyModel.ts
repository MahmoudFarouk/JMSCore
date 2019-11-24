export interface JourneyModel {
    id: number;
    title?: any;
    isTruckTransport: boolean;
    journeyStatus: number;
    fromDestination?: any;
    fromLat?: any;
    fromLng?: any;
    toDistination?: any;
    toLat?: any;
    toLng?: any;
    startDate?: any;
    deliveryDate?: any;
    cargoWeight?: any;
    cargoPriority: number;
    cargoSeverity: number;
    cargoType?: any;
    userId: string;
    isThirdParty: boolean;
   

}
