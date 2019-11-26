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