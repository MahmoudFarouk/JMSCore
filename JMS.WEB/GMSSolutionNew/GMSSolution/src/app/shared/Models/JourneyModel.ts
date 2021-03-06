import { QuestionCategory } from '../enums/question-category.enum';
import { JourneyStatus } from '../enums/journey-status.enum';
import { Level } from '../enums/level.enum';
import { JourneyUpdate } from './JourneyUpdateModel';
import { Checkpoint } from './CheckpointModel';
import { AssessmentQuestion } from './AssessmentQuestionModel';

export interface JourneyModel {
    id?: number;
    title?: string;
    isTruckTransport?: boolean;
    journeyStatus?: JourneyStatus;
    fromDestination?: string;
    fromLat?: number;
    fromLng?: number;
    toDestination?: string;
    toLat?: number;
    toLng?: number;
    startDate?: Date;
    deliveryDate?: Date;
    cargoWeight?: any;
    cargoPriority?: Level;
    cargoSeverity?: Level;
    cargoType?: any;
    userId?: string;
    userFullname?: string;
    isThirdParty?: boolean;
    journeyUpdates?: JourneyUpdate[];
    checkpoints?: Checkpoint[];
    assesments?: AssessmentQuestion[]
    assessmentQuestion?: AssessmentQuestion[],
    riskStatus?:number,
    isNight?:boolean,
    driverName?:string,
    recjectReason?:string,
    closeReason?:string
}
