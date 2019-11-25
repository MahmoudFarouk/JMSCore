import { QuestionCategory } from '../enums/question-category.enum';
import { JourneyStatus } from '../enums/journey-status.enum';
import { Level } from '../enums/level.enum';

export interface AssessmentResult {
    id: number;
    questionId: number;
    isYes: boolean;
    comment: string;
    submittedBy: string;
    submittedByname: string;
    vehicleNo: number;
    journeyUpdateId: number;
}

export interface AssessmentQuestion {
    id: number;
    question: string;
    category: QuestionCategory;
    weight: number;
    isThirdParty: boolean;
    checkpointId: number;
    assessmentResults: AssessmentResult[];
}

export interface JourneyUpdate {
    id: number;
    date: Date;
    journeyId: number;
    journeyStatus: number;
    vehicleNo: string;
    driverId: string;
    drivername: string;
    isJourneyCheckpoint: boolean;
    checkpointId: number;
    riskLevel: number;
    latitude?: any;
    longitude?: any;
    isDriverStatus: boolean;
    isAlert: boolean;
    statusMessage?: any;
    assessmentQuestions: AssessmentQuestion[];
}

export interface CheckPoint {
    id: number;
    name: string;
    lat?: any;
    lng?: any;
    isThirdParty: boolean;
}

export interface Data {
    id: number;
    title: string;
    isTruckTransport: boolean;
    journeyStatus: JourneyStatus;
    fromDestination: string;
    fromLat: number;
    fromLng: number;
    toDestination: string;
    toLat: number;
    toLng: number;
    startDate: Date;
    deliveryDate: Date;
    cargoWeight?: any;
    cargoPriority: Level;
    cargoSeverity: Level;
    cargoType?: any;
    userId: string;
    userFullname: string;
    isThirdParty: boolean;
   // journeyUpdates: JourneyUpdate[];
    //checkPoints: CheckPoint[];
    assesments:any[]
}
