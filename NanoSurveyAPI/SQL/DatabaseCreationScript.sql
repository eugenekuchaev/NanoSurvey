-- Creating tables
CREATE TABLE "Surveys"
(
	"SurveyId" SERIAL,
	"SurveyTitle" VARCHAR(250) NOT NULL,
	"SurveyDescription" VARCHAR(1000),
	"IsActive" BOOLEAN DEFAULT TRUE,

	CONSTRAINT "PK_Survey_SurveyId" PRIMARY KEY("SurveyId")
);

CREATE TABLE "Questions"
(
	"QuestionId" SERIAL,
	"QuestionText" VARCHAR(500) NOT NULL,
	"SurveyId" INTEGER,

	CONSTRAINT "PK_Question_QuestionId" PRIMARY KEY("QuestionId"),
	CONSTRAINT "FK_Question_Survey" FOREIGN KEY("SurveyId") REFERENCES "Surveys"("SurveyId")
);

CREATE TABLE "Answers"
(
	"AnswerId" SERIAL,
	"AnswerText" VARCHAR(500) NOT NULL,
	"IsCorrect" BOOLEAN DEFAULT FALSE,
	"QuestionId" INTEGER,

	CONSTRAINT "PK_Answer_AnswerId" PRIMARY KEY("AnswerId"),
	CONSTRAINT "FK_Answer_Question" FOREIGN KEY("QuestionId") REFERENCES "Questions"("QuestionId")
);

CREATE TABLE "Interviews"
(
	"InterviewId" SERIAL,
	"InterviewDate" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsFinished" BOOLEAN DEFAULT FALSE,
	"SurveyId" INTEGER,

	CONSTRAINT "PK_Interview_InterviewId" PRIMARY KEY("InterviewId"),
	CONSTRAINT "FK_Interview_Survey" FOREIGN KEY("SurveyId") REFERENCES "Surveys"("SurveyId")
);

CREATE TABLE "Results"
(
	"ResultId" SERIAL,
	"SurveyId" INTEGER,
	"QuestionId" INTEGER,
	"AnswerId" INTEGER,
    "InterviewId" INTEGER,

	CONSTRAINT "PK_Result_ResultId" PRIMARY KEY("ResultId"),
	CONSTRAINT "FK_Result_Survey" FOREIGN KEY("SurveyId") REFERENCES "Surveys"("SurveyId"),
	CONSTRAINT "FK_Result_Question" FOREIGN KEY("QuestionId") REFERENCES "Questions"("QuestionId"),
	CONSTRAINT "FK_Result_Answer" FOREIGN KEY("AnswerId") REFERENCES "Answers"("AnswerId"),
    CONSTRAINT "FK_Result_Interview" FOREIGN KEY("InterviewId") REFERENCES "Interviews"("InterviewId")
);

CREATE INDEX "Idx_Question_SurveyId" ON "Questions"("SurveyId");
CREATE INDEX "Idx_Answer_QuestionId" ON "Answers"("QuestionId");

-- Seeding data
INSERT INTO "Surveys" ("SurveyTitle", "SurveyDescription")
VALUES 
(
    'Consumer Preferences and Buying Habits',
    'The survey consists of five multiple-choice questions focusing on age group, 
    preferred shopping channels, purchasing influencers, and openness to trying 
    new products.'
);

INSERT INTO "Questions" ("QuestionText", "SurveyId")
VALUES
    ('How old are you?', 1),
    ('Your gender?', 1),
    ('What shopping channels do you prefer?', 1),
    ('What influences your purchasing decisions the most?', 1),
    ('How often do you try new products or brands?', 1);

INSERT INTO "Answers" ("AnswerText", "QuestionId")
VALUES
    ('18-24 years old', 1),
    ('25-34 years old', 1),
    ('35-44 years old', 1),
    ('45-54 years old', 1),
    ('55 years old and above', 1),
    ('Male', 2),
    ('Female', 2),
    ('Physical retail stores', 3),
    ('Online marketplaces', 3),
    ('Brand websites', 3),
    ('Social media platforms', 3),
    ('Mobile apps', 3),
    ('Price', 4),
    ('Brand reputation', 4),
    ('Product quality', 4),
    ('Customer reviews and ratings', 4),
    ('Product features and specifications', 4),
    ('Promotional offers and discounts', 4),
    ('Personal recommendations', 4),
    ('Very often (Always looking for new options)', 5),
    ('Occasionally (Open to trying new things occasionally)', 5),
    ('Rarely (Prefer sticking to familiar products)', 5),
    ('Never (Stick to the same products/brands consistently)', 5);
