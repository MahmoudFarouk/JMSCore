import { AssessmentResult } from './assessment-result';
import { QuestionCategory } from './question-category.enum';
import { Journey } from './journey';
import { Checkpoint } from './checkpoint';

export class AssessmentQuestion {
  Id: Number;
  Question:string;
  Weight:number;
  CheckpointId:number;
  JourneyId:number;
  IsThirdParty:boolean;
  Checkpoint:Checkpoint;
  Category:QuestionCategory;
  Journey:Journey;
  AssessmentResult:AssessmentResult[];

}
