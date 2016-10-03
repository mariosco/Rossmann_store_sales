# Rossmann store sales

An implementation of linear regression in **F#** used for predicting the sales of Rossmann in the following Kaggle competition: https://www.kaggle.com/c/rossmann-store-sales. A lot of techniques, inspiration and motivation came from the workshop **'Machine learning and functional programming'** by **Mathias Brandewinder** in the conference **Build Stuff 2015**.  

## Overview

The goal of this competition was to predict the number of sales of 1115 Rossmann stores for a period of six weeks (01.08.15-17.09.15). The training data provided was for the period 01.01.13-31.07.15 and included features like the date, whether the store did a promotion and the number of sales and customers.

By coding this simple application which implements linear regression using `MathNet.Numerics` I could get a mean error of `0.15522` in the prediction, compared to the `0.10021` of the first place winner. This was impressive in the sense that one can get very fast practically useful results using simple methods that can easily be integrated to production code.

When running the application one has to options. The first one is to make evaluations for specific stores and the second one to evaluate the model for all stores. The evaluation is done using the original features, plus a feature testing if the date is in a period before Christmas, powers of the day of week, and the product of the day of week and promotion. 

## Setup

Run `Rossmann\bin\Debug\Rossmann.exe` once and it will create the directories `Datasets`, `Features`, `InitialDataset` and `RawData` in `C:\Temp\RossmannData\`. Then download the `train.csv` from https://www.kaggle.com/c/rossmann-store-sales and add it to `RawData`. Then run again `Rossmann\bin\Debug\Rossmann.exe` and you are set.

## 

## License

Copyright © 2016 Marios Koulakis

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>
