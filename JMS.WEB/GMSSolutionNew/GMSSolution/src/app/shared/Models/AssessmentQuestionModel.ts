import { AssessmentResult } from './AssessmentResultModel';
import { QuestionCategory } from '../enums/question-category.enum';

export interface AssessmentQuestion {
    id: number;
    question: string;
    category: QuestionCategory;
    weight: number;
    isThirdParty: boolean;
    checkpointId: number;
    assessmentResults: AssessmentResult[];
    assessmentResult: AssessmentResult[];
}
