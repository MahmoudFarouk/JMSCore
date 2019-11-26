import { Checkpoint } from './CheckpointModel';
import { AssessmentQuestion } from './AssessmentQuestionModel';

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
    checkpoint:Checkpoint;
    assessmentQuestions: AssessmentQuestion[];
}
