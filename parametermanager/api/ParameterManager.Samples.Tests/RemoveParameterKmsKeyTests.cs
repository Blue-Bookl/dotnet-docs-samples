/*
 * Copyright 2025 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Google.Cloud.Kms.V1;
using Google.Cloud.ParameterManager.V1;
using Google.Protobuf;
using System.Text;

[Collection(nameof(ParameterManagerFixture))]
public class RemoveParameterKmsKeyTests
{
    private readonly ParameterManagerFixture _fixture;
    private readonly RemoveParameterKmsKeySample _sample;

    public RemoveParameterKmsKeyTests(ParameterManagerFixture fixture)
    {
        _fixture = fixture;
        _sample = new RemoveParameterKmsKeySample();
        _fixture.GetKeyRing(_fixture.ProjectId, ParameterManagerFixture.KeyRingId);
    }

    [Fact]
    public void RemoveParameterKmsKey()
    {
        string KeyId = _fixture.RandomId();
        CryptoKey cryptoKey = _fixture.CreateHsmKey(_fixture.ProjectId, KeyId, ParameterManagerFixture.KeyRingId);

        string parameterId = _fixture.RandomId();
        _fixture.CreateParameterWithKmsKey(parameterId, ParameterFormat.Unformatted, cryptoKey.Name);

        Parameter result = _sample.RemoveParameterKmsKey(
            projectId: _fixture.ProjectId, parameterId: parameterId);

        Assert.NotNull(result);
        Assert.Equal(parameterId, result.ParameterName.ParameterId);
    }
}
