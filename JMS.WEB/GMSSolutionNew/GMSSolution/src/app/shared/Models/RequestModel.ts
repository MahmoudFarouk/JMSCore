import { JourneyStatus } from '../enums/journey-status.enum';

export interface RequestModel {
    journeyId: number;
    journeyTitle: string;
    fromDestination: string;
    toDestination: string;
    creationDate: Date;
    deliveryDate: Date;
    status:JourneyStatus
}